import React, { useState, useEffect } from 'react';
import { Link } from 'react-router-dom';
import SuperMetroidServices from '../services/SuperMetroid';
import { setTokenHeaders } from '../utils/Authentication';

function GamesPage() {
    const [games, setGames] = useState([]);

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
        alert('Link copiado com sucesso!');
    };

    return (
        <div className="container">
            <table className="table mt-4">
                <thead>
                    <tr>
                        <th scope="col">Criação do jogo</th>
                        <th scope="col">Jogadores</th>
                        <th scope="col">Tracker</th>
                        <th scope="col">Restream</th>
                    </tr>
                </thead>
                <tbody>
                    {games.map((game) => (
                        <tr key={game.id}>
                            <td>{new Date(game.createdAt).toLocaleString()}</td>
                            <td>{game.players.join(', ')}</td>
                            <td><Link to={`/game/${game.id}`} className="btn btn-primary me-2">Abrir tracker</Link>
                                <button onClick={() => { handleCopy(`/game/${game.id}`) }} className="btn btn-secondary">Copiar link</button></td>
                            <td><Link to={`/game/readonly/${game.id}`} className="btn btn-primary me-2">Abrir tracker</Link>
                                <button onClick={() => { handleCopy(`/game/readonly/${game.id}`) }} className="btn btn-secondary">Copiar link</button></td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default GamesPage;