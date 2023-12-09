import './GamesPage.scss';
import React, { useState, useEffect } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import SuperMetroidServices from '../services/SuperMetroid';
import { setTokenHeaders, isLogin } from '../utils/Authentication';
import Form from 'react-bootstrap/Form';
import Button from 'react-bootstrap/Button';
import Auth from '../services/Auth';

function GamesPage() {
    const [games, setGames] = useState([]);
    const navigate = useNavigate();

    const [editGameId, setEditGameId] = useState(null);
    const [editDescription, setEditDescription] = useState('');
    const [editPlayers, setEditPlayers] = useState('');
    const [isAdmin, setIsAdmin] = useState(false);

    useEffect(() => {
        const checkUserStatus = async () => {

            var hasToken = isLogin();

            if (!hasToken) {
                navigate('/loginpage');
            }
        };

        checkUserStatus();
    }, [navigate]);

    useEffect(() => {

        setTokenHeaders();
        
        SuperMetroidServices.activeGames()
            .then((result) => {
                setGames(result.data);
            });

        Auth.isadmin()
            .then((result) => {
                var isAdmin = result.data.isAdmin;
                setIsAdmin(isAdmin);
            });

    }, []);

    const handleCopy = (url) => {

        const fullURL = window.location.origin + url;
        navigator.clipboard.writeText(fullURL);
        toast.success('Link copiado com sucesso!');
    };

    const handleEdit = (game) => {
        setEditGameId(game.id);
        setEditDescription(game.description);
        setEditPlayers(game.players.join(', '));
    };

    const handleSave = () => {        

        const hasLongString = editPlayers.split(', ').some(player => player.trim().length > 20);

        if (!hasLongString) {

            var payload = {
                Description: editDescription.trim(),
                PlayerNames: editPlayers.split(', ').map(player => player.replace(',', '').trim()),
            }

            SuperMetroidServices.update(editGameId, payload)
                .then(response => {

                    setGames(games.map(game => game.id === editGameId ? response.data : game));
                    setEditGameId(null);
                    setEditDescription('');
                    setEditPlayers('');

                    toast.success('Jogo atualizado com sucesso!');
                })
                .catch(error => {

                    if (error && error.response && error.response.data && error.response.data.message)
                        toast.warning(error.response.data.message);

                    else {

                        console.log("ERROR", "SuperMetroidServices.update", error);
                        toast.error('Erro inesperado ao atualizar o jogo.');
                    }
                });
        } else {
            toast.warning("O nome não pode conter mais do que 20 caracteres");
        }
    };

    return (
        <div className="container">
            <table className="table mt-4">
                <thead>
                    <tr style={{ borderColor: '#BBBBBB' }}>
                        <th scope="col">Descrição</th>
                        <th scope="col">Jogadores</th>
                        {isAdmin && <th>Editar</th>}
                        <th scope="col">Tracker</th>
                        <th scope="col">Restreamer</th>
                    </tr>
                </thead>
                <tbody>
                    {games.map((game) => (
                        <tr key={game.id} style={{ borderColor: '#BBBBBB' }}>
                            <td>
                                {editGameId === game.id ? (
                                    <Form.Control
                                        type="text"
                                        value={editDescription}
                                        onChange={e => setEditDescription(e.target.value)}
                                    />
                                ) : (
                                    game.description
                                )}
                            </td>
                            <td>
                                {editGameId === game.id ? (
                                    <Form.Control
                                        type="text"
                                        value={editPlayers}
                                        onChange={e => setEditPlayers(e.target.value)}
                                    />
                                ) : (
                                    game.players.join(', ')
                                )}
                            </td>
                            {isAdmin && (
                                <td>
                                    {editGameId === game.id ? (
                                        <Button variant="success" onClick={handleSave}>Save</Button>
                                    ) : (
                                        <Button variant="primary" onClick={() => handleEdit(game)}>Editar</Button>
                                    )}
                                </td>
                            )}
                            <td><Link to={`/game/${game.id}`} className="btn btn-primary me-2">Abrir</Link>
                                <button onClick={() => { handleCopy(`/game/${game.id}`) }} title={"Copiar Link"} className="btn btn-secondary btn-copy">Copiar Link</button></td>
                            <td><Link to={`/game/readonly/${game.id}`} className="btn btn-primary me-2">Abrir</Link>
                                <button onClick={() => { handleCopy(`/game/readonly/${game.id}`) }} title={"Copiar Link"} className="btn btn-secondary btn-copy">Copiar Link</button></td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default GamesPage;