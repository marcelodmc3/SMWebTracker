﻿/* eslint-disable import/no-anonymous-default-export */
import api from './api'

export default {

    async activeGames() {
        return await api.get(`api/supermetroid/game/active`);
    },

    async trackerById(trackerId) {
        return await api.get(`api/supermetroid/tracker/${trackerId}`);
    },

    async gameById(gameId) {
        return await api.get(`api/supermetroid/game/${gameId}`);
    },

    async track(trackerId, payload) {
        return await api.patch(`api/supermetroid/tracker/${trackerId}`, payload);
    },

    async update(gameindex, payload) {
        return await api.patch(`api/supermetroid/game/${gameindex}`, payload);
    },
};