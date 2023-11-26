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

    return (
        <div className="container">
            <table className="table mt-4">
                <thead>
                    <tr>
                        <th scope="col">Criação do jogo</th>
                        <th scope="col">Jogadores</th>
                        <th scope="col">Link do tracker</th>
                        <th scope="col">Link para restream</th>
                    </tr>
                </thead>
                <tbody>
                    {games.map((game) => (
                        <tr key={game.id}>
                            <td>{new Date(game.createdAt).toLocaleString()}</td>
                            <td>{game.players.join(', ')}</td>
                            <td><Link to={`/game/${game.id}`}>Track Game</Link></td>
                            <td><Link to={`/game/stream/${game.id}`}>Stream Game</Link></td>
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default GamesPage;