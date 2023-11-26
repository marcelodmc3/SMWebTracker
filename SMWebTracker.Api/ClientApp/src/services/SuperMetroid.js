/* eslint-disable import/no-anonymous-default-export */
import api from './api'

export default {

    async game(gameindex) {
        return await api.get(`api/supermetroid/game/${gameindex}`);
    },

    async activeGames() {
        return await api.get(`api/supermetroid/game/active`);
    }
};