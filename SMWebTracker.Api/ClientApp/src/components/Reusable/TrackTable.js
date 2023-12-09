import './TrackTable.scss';
import React, { useState, useEffect } from 'react';
import SuperMetroidServices from '../../services/SuperMetroid';
import { setTokenHeaders } from '../../utils/Authentication';
import 'react-toastify/dist/ReactToastify.css';
import Dropdown from 'react-bootstrap/Dropdown';

function TrackTable({ id }) {
    const [data, setData] = useState(null);
    const [images, setImages] = useState([]);
    const [selectedOption, setSelectedOption] = useState(null);

    const options =
        [
            {
                "DisplayName": "Selecione o resultado",
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
        return () => clearInterval(interval);
    }, [id]);

    const handleClick = (imageName) => {

        var payload = {};
        payload[imageName] = true;

        SuperMetroidServices.track(id, payload)
            .then((result) => {
                setData(result.data);
                setSelectedOption(result.data["position"]);
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

    function getSelectedOptionName() {

        if (selectedOption)
        {
            for (let i = 0; i < options.length; i++)
            {
                var option = options[i];                

                if (String(option.OptionId) === String(selectedOption))
                    return option.DisplayName;
            }
        }

        return "Selecione o resultado";
    }

    const handleSelect = (optionId) => {
                
        var payload = {};
        payload["Position"] = optionId;

        SuperMetroidServices.track(id, payload)
            .then((result) => {
                setData(result.data);
                setSelectedOption(optionId);
                setSelectedOption(result.data["position"]);
            })
            .catch(() => {
            });
    };
   
    return (
        <div className="container bg-dark text-white" style={{ minWidth: 230 }}>
            <h5 className="table-cell-header text-center text-size-tracker my-0">{padString(data.playerName)}</h5>
            <Dropdown className = "text-center" onSelect={handleSelect}>
                <Dropdown.Toggle variant="success" id="dropdown-basic">
                    {getSelectedOptionName()}
                </Dropdown.Toggle>

                <Dropdown.Menu>
                    {options.map((option, index) => (
                        <Dropdown.Item key={index} eventKey={option.OptionId}>{option.DisplayName}</Dropdown.Item>
                    ))}
                </Dropdown.Menu>
            </Dropdown>
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