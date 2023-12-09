import React, { useState, useEffect } from 'react';
import {  useNavigate } from 'react-router-dom';
import { useParams } from 'react-router-dom';
import TrackTable from './Reusable/TrackTable';
import TrackTableReadOnly from './Reusable/TrackTableReadOnly';
import SuperMetroidServices from '../services/SuperMetroid';
import { setTokenHeaders, isLogin } from '../utils/Authentication';

function GamePage({ readonly }) {
    const [ids, setIds] = useState([]);
    const { id } = useParams();
    const isReadOnly = readonly;
    const navigate = useNavigate();

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

        SuperMetroidServices.gameById(id)
            .then((result) => {
                setIds(result.data.superMetroidTrackers.map(t => t.id));
            });
    }, []);

    const rows = [];

    var cellsInaRow = 2;
    var width = 460;

    if (ids.length > 4) {
        cellsInaRow = 3;
        width = 720;
    }

    for (let i = 0; i < ids.length; i += cellsInaRow) {
        rows.push(ids.slice(i, i + cellsInaRow));
    }

    if (isReadOnly) {
        return (
            <div className="container">
                <table className="table mt-4" style={{ minWidth: width }}>
                    <tbody>
                        {rows.map((row, i) => (
                            <tr key={i} style={{ borderColor: '#BBBBBB' }}>
                                {row.map((id) => (
                                    <td key={id} style={{ padding: '0.1rem' }}>
                                        <TrackTableReadOnly id={id} />
                                    </td>
                                ))}
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        );
    } else {
        return (
            <div className="container ">
                <table className="table mt-4">
                    <tbody>
                        {rows.map((row, i) => (
                            <tr key={i} style={{ borderColor: '#BBBBBB' }}>
                                {row.map((id) => (
                                    <td key={id} style={{ padding: '0.1rem' }}>
                                        <TrackTable id={id} />
                                    </td>
                                ))}
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        );
    }
}

export default GamePage;
