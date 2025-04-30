import { post } from './apiClient';

const searchService = () => {
  const fetchPositions = async (searchData) => {
    try {
      const resp = await post('/api/search/positions', searchData);
      return resp.data;
    } catch (error) {
      if (error.response) {
        if (error.response.status === 400) {
          throw new Error(`Bad Request: ${error.response.data}`);
        } else if (error.response.status === 500) {
          throw new Error(`Internal Server Error: ${error.response.data}`);
        } else {
          console.log(error.response.data);
          console.log(error.response.status);
          console.log(error.response.headers);
          throw new Error('Unknown Error');
        }
      } else if (error.request) {
        console.log(error.request);
        throw new Error('No response received');
      } else {
        console.log(error.message);
        throw new Error('Unknown Error');
      }
    }
  };

  return { fetchPositions };
};

export default searchService();
