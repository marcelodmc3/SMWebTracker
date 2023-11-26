import axios from 'axios';
import { isLogin } from '../utils/Authentication';

export const HttpStatus = {
    UNAUTHORIZED: 401,
    FORBIDDEN: 403,
    NOT_FOUND: 404
  };  

const isHandlerEnabled = (config = {}) => {

    return config.hasOwnProperty('handlerEnabled') && !config.handlerEnabled ? 
      false : true
  }

const errorHandler = (error) => {

    if (isHandlerEnabled(error.config)) {
        if ((error.response && error.response.status === HttpStatus.FORBIDDEN) ||
            ((error.reject && error.re.status === HttpStatus.UNAUTHORIZED && isLogin()))) {
            localStorage.clear();
            window.location.replace("/")
        }
    }
    return Promise.reject({ ...error })
}

const successHandler = (response) => {   
    return response
  }

const fetchClient = () => {

    const defaultOptions = {
        baseURL: "/",
        responseType: "json"
    };

    let instance = axios.create(defaultOptions);

    instance.interceptors.request.use(function (config) {
        config.headers.Authorization = axios.defaults.headers.common['Authorization'] ? axios.defaults.headers.common['Authorization'] : '';
        return config;
    });

    instance.interceptors.response.use(
        response => successHandler(response),
        error => errorHandler(error)
    )
    return instance;
};

export default fetchClient();