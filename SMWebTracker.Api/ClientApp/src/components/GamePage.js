import React, { useState, useEffect } from 'react';
import { useParams } from 'react-router-dom';
import TrackTable from './Reusable/TrackTable';
import TrackTableReadOnly from './Reusable/TrackTableReadOnly';
import SuperMetroidServices from '../services/SuperMetroid';
import { setTokenHeaders } from '../utils/Authentication';


function GamePage({ readonly }) {
    const [ids, setIds] = useState([]);
    const { id } = useParams();
    const isReadOnly = readonly;

    console.log("readonly", isReadOnly);

    useEffect(() => {

        setTokenHeaders();
        console.log("GamePage", id);

        SuperMetroidServices.game(id)
            .then((result) => {
                setIds(result.data.superMetroidTrackers.map(t => t.id));
            });
    }, []);

    const rows = [];
    for (let i = 0; i < ids.length; i += 2) {
        rows.push(ids.slice(i, i + 2));
    }

    if (isReadOnly) {
        return (
            <div className="container">
                <table className="table mt-4" style={{ minWidth: 460 }}>
                    <tbody>
                        {rows.map((row, i) => (
                            <tr key={i} >
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
            <div className="container">
                <table className="table mt-4">
                    <tbody>
                        {rows.map((row, i) => (
                            <tr key={i} >
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
