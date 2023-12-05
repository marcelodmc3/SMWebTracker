import './TrackTableReadOnly.scss';
import React, { useState, useEffect } from 'react';
import SuperMetroidServices from '../../services/SuperMetroid';
import { setTokenHeaders } from '../../utils/Authentication';
function TrackTableReadOnly({ id }) {
    const [data, setData] = useState(null);
    const [images, setImages] = useState([]);
    const [countdown, setCountdown] = useState(5);

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
                console.error('Failed to fetch count', error);
            }
        }, 5000);

        const countdownInterval = setInterval(() => {
            setCountdown((countdown) => countdown - 1);
        }, 1000);

        
        // Clean up interval on unmount
        return () => {
            clearInterval(interval); clearInterval(countdownInterval);
        }
    }, [id]);

    if (!data) {
        return null;
    }

    const rows = [];
    for (let i = 0; i < images.length; i += 4) {
        rows.push(images.slice(i, i + 4));
    }

    return (
        <div className="container bg-dark text-white" style={{height:305, maxWidth:200}}>
            <h4 className="text-center my-0">{data.playerName}</h4>
            <table className="table">
                <tbody>
                    {rows.map((row, i) => (
                        <tr key={i} style={{ border:'none' }}>
                            {row.map((imageName) => (
                                <td key={imageName} style={{padding:'0.2rem'} }>
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

export default TrackTableReadOnly;