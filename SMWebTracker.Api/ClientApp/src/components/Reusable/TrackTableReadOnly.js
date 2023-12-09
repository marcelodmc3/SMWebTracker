import './TrackTableReadOnly.scss';
import React, { useState, useEffect } from 'react';
import SuperMetroidServices from '../../services/SuperMetroid';
import { setTokenHeaders } from '../../utils/Authentication';
function TrackTableReadOnly({ id }) {
    const [data, setData] = useState(null);
    const [images, setImages] = useState([]);
    const [selectedOption, setSelectedOption] = useState(null);

    const options =
        [
            {
                "DisplayName": "-",
                "OptionId": 0
            },
            {
                "DisplayName": "1º LUGAR",
                "OptionId": 1
            },
            {
                "DisplayName": "2º LUGAR",
                "OptionId": 2
            },
            {
                "DisplayName": "3º LUGAR",
                "OptionId": 3
            },
            {
                "DisplayName": "4º LUGAR",
                "OptionId": 4
            },
            {
                "DisplayName": "DESISTIU",
                "OptionId": 98
            },
            {
                "DisplayName": "DESCLASSIFICADO",
                "OptionId": 99
            }
        ];

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
                setSelectedOption(result.data["position"]);
            });

    }, [id]);

    useEffect(() => {

        const interval = setInterval(async () => {
            try {
                SuperMetroidServices.trackerById(id)
                    .then((result) => {
                        setData(result.data);
                        setSelectedOption(result.data["position"]);
                    });

            } catch (error) {
                console.error('Erro ao atualizar o tracker', error);
            }
        }, 5000);
       
        // Clean up interval on unmount
        return () => {
            clearInterval(interval);
        }
    }, [id]);

    if (!data) {
        return null;
    }

    const rows = [];
    for (let i = 0; i < images.length; i += 4) {
        rows.push(images.slice(i, i + 4));
    }

    function getSelectedOptionName() {

        if (selectedOption) {
            for (let i = 0; i < options.length; i++) {
                var option = options[i];

                if (String(option.OptionId) === String(selectedOption))
                    return option.DisplayName;
            }
        }

        return "-";
    }

    function getResultFontStyle() {

        if (selectedOption) {

            if (String(selectedOption) === String(0))
                    return "text-center my-0 result-text-default";

            if (String(selectedOption) === String(1))
                    return "text-center my-0 result-text-winner";

            if (String(selectedOption) === String(98) || String(selectedOption) === String(99))
                    return "text-center my-0 result-text-dnfs";                            

            return "text-center my-0 result-text-runnerups";
        }

        return "text-center my-0 result-text-default";
    }

    return (
        <div className="container bg-dark text-white" style={{ height: 325, maxWidth: 200, minWidth: 200 }}>            
            <p className="text-center my-0 text-size-read-only">{data.playerName}</p>
            <h5 className={getResultFontStyle()}>{getSelectedOptionName()}</h5>
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