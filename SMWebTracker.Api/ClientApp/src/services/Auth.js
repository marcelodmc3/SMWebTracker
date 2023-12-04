/* eslint-disable import/no-anonymous-default-export */
import api from './api'

export default {

    async login(command) {
        return await api.post(`api/auth/login`, command);
    },

    async ping() {
        return await api.get(`api/auth/ping`);
    }
};