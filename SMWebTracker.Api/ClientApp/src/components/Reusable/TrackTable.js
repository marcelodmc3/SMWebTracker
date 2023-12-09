import './TrackTable.scss';
import React, { useState, useEffect } from 'react';
import SuperMetroidServices from '../../services/SuperMetroid';
import { setTokenHeaders } from '../../utils/Authentication';
import 'react-toastify/dist/ReactToastify.css';

function TrackTable({ id }) {
    const [data, setData] = useState(null);
    const [images, setImages] = useState([]);

    useEffect(() => {

        setImages([
            "kraid",
            "sporeSpawn",
            "morphBall",
            "highJumpBoots",
            "phantoon",
            "crocomire",
            "bombs",
            "speedBooster",
            "draygon",
            "botwoon",
            "springBall",
            "spaceJump",
            "ridley",
            "goldenTorizo",
            "chargeBeam",
            "spazerBeam",
            "grapple",
            "xray",
            "iceBeam",
            "waveBeam",
            "variaSuit",
            "gravitySuit",
            "screwAttack",
            "plasmaBeam"
        ]);

        setTokenHeaders();

        SuperMetroidServices.trackerById(id)
            .then((result) => {
                setData(result.data);
            });

    }, [id]);

    useEffect(() => {
        const interval = setInterval(async () => {
            try {
                SuperMetroidServices.trackerById(id)
                    .then((result) => {
                        setData(result.data);
                    });

            } catch (error) {
                console.error('Erro ao atualizar o tracker', error);
            }
        }, 5000);

        // Clean up interval on unmount
        return () => clearInterval(interval);
    }, [id]);

    const handleClick = (imageName) => {

        var payload = {};
        payload[imageName] = true;

        SuperMetroidServices.track(id, payload)
            .then((result) => {
                setData(result.data);
            })
            .catch(() => {
            });
    }

    if (!data) {
        return null;
    }

    const rows = [];
    for (let i = 0; i < images.length; i += 4) {
        rows.push(images.slice(i, i + 4));
    }

    function padString(str) {
        const totalLength = 20;
        const padLength = Math.ceil((totalLength - str.length) / 2);

        let paddedStr = str.padStart(str.length + padLength, ' ');
        paddedStr = paddedStr.padEnd(totalLength, ' ');

        return paddedStr;
    }

    function camelToRegular(str) {        
        return str.replace(/([A-Z])/g, ' $1').trim().replace(/^./, str => str.toUpperCase());
    }

    return (
        <div className="container bg-dark text-white" style={{ minWidth: 230 }}>
            <h5 className="table-cell-header text-center text-size-tracker my-0">{padString(data.playerName)}</h5>
            <table className="table">
                <tbody>
                    {rows.map((row, i) => (
                        <tr key={i} style={{ border:'none' }}>
                            {row.map((imageName) => (
                                <td
                                    key={imageName} style={{ padding: '0.2rem', textAlign: 'center' }}
                                    onClick={() => { handleClick(imageName) }}
                                    title={camelToRegular(imageName)}
                                >
                                    <img src={`/images/${imageName}.png`} alt={imageName} className={`img-fluid ${data[imageName] ? '' : 'grayscale'}`} />
                                </td>
                            ))}
                        </tr>
                    ))}
                </tbody>
            </table>
        </div>
    );
}

export default TrackTable;