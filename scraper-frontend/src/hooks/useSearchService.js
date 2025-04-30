import { useState } from 'react';
import searchService from '../services/searchService';

const useSearchService = () => {
  const [positionList, setPositionList] = useState(null);
  const [isLoading, setIsLoading] = useState(false);
  const [errorMessage, setErrorMessage] = useState(null);

  const fetchPositions = async ({ keywords, url, searchEngine }) => {
    try {
      setIsLoading(true);
      setErrorMessage(null);
      const resp = await searchService.fetchPositions({
        keywords,
        url,
        searchEngine,
      });
      setPositionList(resp.positions);
    } catch (error) {
      console.log(`Failed to fetch positions, ${error.message}`);
      setErrorMessage(`Failed to fetch positions.`);
    } finally {
      setIsLoading(false);
    }
  };

  return { positionList, isLoading, errorMessage, fetchPositions };
};

export default useSearchService;
