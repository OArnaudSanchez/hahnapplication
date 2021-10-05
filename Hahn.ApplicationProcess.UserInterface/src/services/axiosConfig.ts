import axios from 'axios';

const baseURL: string = 'https://localhost:44306/api';

export default axios.create({
    baseURL,
});