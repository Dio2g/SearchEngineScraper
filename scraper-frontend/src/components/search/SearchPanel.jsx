import SearchForm from './SearchForm';
import useSearchService from '../../hooks/useSearchService';
import SearchDisplay from './SearchDisplay';

const SearchPanel = () => {
  const { fetchPositions, positionList, isLoading, errorMessage } =
    useSearchService();

  const onSubmitSearchData = async (keywords, url, searchEngine) => {
    await fetchPositions({ keywords, url, searchEngine });
  };

  return (
    <div>
      <SearchForm onSubmitSearchData={onSubmitSearchData} />
      <SearchDisplay
        positionList={positionList}
        isLoading={isLoading}
        errorMessage={errorMessage}
      />
    </div>
  );
};

export default SearchPanel;
