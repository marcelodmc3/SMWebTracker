import axios from 'axios';

export const TOKEN_KEY = 'jwt_smwebtracker';

export const LOGIN_URL = "/";

export const setTokenHeaders = () => {

    console.log("setTokenHeaders");

    if(!isLogin()){
        login();
      }
      clearQueryString();
      var token = getAccessToken();  
      if (token) {    
        axios.defaults.headers.common['Authorization'] = `Bearer ${token}`;        
        return true;
      }
    axios.defaults.headers.common['Authorization'] = null;
    return false;   
}

export const login = () => {
    const queryParams = getQueryString();
    if (queryParams.has('accessToken')){
        const accessToken = queryParams.get('accessToken');
        localStorage.setItem(TOKEN_KEY, accessToken);
    }
}

export const logout = () => {
    localStorage.removeItem(TOKEN_KEY);
}

export const isLogin = () => {
    if (localStorage.getItem(TOKEN_KEY)) {
        return true;    
    }
    return false;
}

export const setToken = (token) => {
    localStorage.setItem(TOKEN_KEY, token);
}

export const getAccessToken = () => {
    if (localStorage.getItem(TOKEN_KEY)){
        return localStorage.getItem(TOKEN_KEY);
    }else {
        return {};
    }   
}

export const getQueryString = () => {
    return new URLSearchParams(window.location.search);
}

export const clearQueryString = () => {
    var uri = window.location.toString();
    var clean_uri = uri.substring(0, uri.indexOf("?"));
    window.history.replaceState({}, document.title, clean_uri);
}

export const getAuthorizationHeader = () => {
    if (localStorage.getItem(TOKEN_KEY)){
        return  {'Authorization': getAccessToken()};
    }else{
        return {};
    }
}
