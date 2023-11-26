import React, { useState } from 'react';
import { useNavigate } from 'react-router-dom';
import AuthService from '../services/Auth';
import { setTokenHeaders, TOKEN_KEY } from '../utils/Authentication';

function LoginPage() {
    const [email, setEmail] = useState('');
    const [password, setPassword] = useState('');
    const [error, setError] = useState(null);
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
        e.preventDefault()

        try {
            AuthService.login({ Login: email, Password: password })
            .then((response) => {

                localStorage.setItem(TOKEN_KEY, response.data.token);
                setTokenHeaders();
                navigate('/');

            }).catch((errorResponse) => {

                console.log("errorResponse", errorResponse);
                setError('The email or password is incorrect');
            });            

        } catch (err) {
            setError('The email or password is incorrect');
        }
    };

    return (
        <div className="container">
            <form onSubmit={handleSubmit} className="mt-4">
                <div className="mb-3">
                    <label htmlFor="email" className="form-label">Email address</label>
                    <input type="email" className="form-control" id="email" value={email} onChange={(e) => setEmail(e.target.value)} required />
                </div>
                <div className="mb-3">
                    <label htmlFor="password" className="form-label">Password</label>
                    <input type="password" className="form-control" id="password" value={password} onChange={(e) => setPassword(e.target.value)} required />
                </div>
                <button type="submit" className="btn btn-primary">Log in</button>
            </form>
            {error && <div className="alert alert-danger mt-3">{error}</div>}
        </div>
    );
}

export default LoginPage;