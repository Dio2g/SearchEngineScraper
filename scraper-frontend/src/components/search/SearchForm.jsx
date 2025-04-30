import { useState } from 'react';

const SearchForm = ({ onSubmitSearchData }) => {
  const [url, setUrl] = useState('');
  const [keywords, setKeywords] = useState('');
  const [searchEngine, setSearchEngine] = useState(0); //Bing as default

  const engineValues = [
    { key: 0, value: 'Bing' },
    { key: 1, value: 'Yahoo' },
  ];

  const handleSubmit = (e) => {
    e.preventDefault();

    if (!keywords) {
      alert('Enter keywords');
      return;
    } else if (!url) {
      alert('Enter URL to find');
      return;
    }

    onSubmitSearchData(keywords, url, searchEngine);
  };

  return (
    <form className="mt-4" onSubmit={handleSubmit}>
      <label className="form-label">
        Keywords:
        <input
          className="form-element"
          type="text"
          name="keywords"
          maxLength={255}
          value={keywords}
          onChange={(e) => setKeywords(e.target.value)}
        />
      </label>
      <label className="form-label">
        URL To Find:
        <input
          className="form-element"
          type="text"
          name="url"
          maxLength={1024}
          value={url}
          onChange={(e) => setUrl(e.target.value)}
        />
      </label>
      <label className="form-label">
        Search Engine:
        <select
          className="form-element"
          name="searchEngine"
          value={searchEngine}
          onChange={(e) => setSearchEngine(parseInt(e.target.value))}
        >
          {engineValues.map((option) => (
            <option value={option.key} key={option.key}>
              {option.value}
            </option>
          ))}
        </select>
      </label>
      <div className="flex justify-center">
        <button
          className="my-4 px-10 py-2 font-light 
           rounded-xl border-2 bg-[#19E8FF] hover:bg-[#08C1C9]"
          type="submit"
        >
          Search
        </button>
      </div>
    </form>
  );
};

export default SearchForm;
