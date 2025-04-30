import axios from 'axios';

const BASE_URL = import.meta.env.VITE_API_BASE_URL;

const searchApiClient = axios.create({
  baseURL: BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
});

const post = (url, data) => {
  return searchApiClient.post(url, data);
};

export { post };
