import React, { useState, useEffect } from 'react';
import { Link, useNavigate } from 'react-router-dom';
import { toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import SuperMetroidServices from '../services/SuperMetroid';
import { setTokenHeaders, isLogin } from '../utils/Authentication';
import Form from 'react-bootstrap/Form';

function GamesPage() {
    const [games, setGames] = useState([]);
    const navigate = useNavigate();

    const [editGameId, setEditGameId] = useState(null);
    const [editDescription, setEditDescription] = useState('');
    const [editPlayers, setEditPlayers] = useState('');
    const [isAdmin, setisAdmin] = useState(false);

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

        var payload = {
            Description: editDescription,
            PlayerNames: editPlayers.split(', ').map(player => player.trim()),
        }

        SuperMetroidServices.activeGames(editGameId, payload)
            .then(response => {
                setGames(games.map(game => game.id === editGameId ? response.data : game));
                setEditGameId(null);
                setEditDescription('');
                setEditPlayers('');
            })
            .catch(error => {
                console.error('There was an error!', error);
            });
    };

    return (
        <div className="container">
            <table className="table mt-4">
                <thead>
                    <tr style={{ borderColor: '#BBBBBB' }}>
                        <th scope="col">Descrição</th>
                        <th scope="col">Jogadores</th>
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
                            <td>{game.players.join(', ')}</td>
                            <td><Link to={`/game/${game.id}`} className="btn btn-primary me-2">Abrir</Link>
                                <button onClick={() => { handleCopy(`/game/${game.id}`) }} className="btn btn-secondary">Link</button></td>
                            <td><Link to={`/game/readonly/${game.id}`} className="btn btn-primary me-2">Abrir</Link>
                                <button onClick={() => { handleCopy(`/game/readonly/${game.id}`) }} className="btn btn-secondary">Link</button></td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default GamesPage;