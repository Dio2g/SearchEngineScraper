import { useEffect, useState } from 'react';

const SearchDisplay = ({ positionList, isLoading, errorMessage }) => {
  const [loadingAnimation, setLoadingAnimation] = useState('.');

  useEffect(() => {
    if (!isLoading) return;

    const animationInterval = setInterval(() => {
      setLoadingAnimation((current) => {
        if (current.length >= 5) {
          return '.';
        }
        return current + '.';
      });
    }, 200);

    return () => clearInterval(animationInterval);
  }, [isLoading]);

  if (positionList === null && !isLoading) {
    return;
  } else if (isLoading) {
    return (
      <div className="display-text">
        Positions Are Loading{loadingAnimation}
      </div>
    );
  }

  if (errorMessage) {
    return <div className="display-text">{errorMessage}</div>;
  }

  if (!positionList || positionList.length === 0) {
    return <div className="display-text">No Positions Found</div>;
  }

  return (
    <div className="flex flex-col px-4 py-3">
      <h3 className="display-text pb-2">URL Found At These Positions:</h3>
      <div className="flex flex-wrap gap-2">
        {positionList.map((position, idx) => (
          <div
            className="min-w-[60px] text-center py-2 px-4 border-2 bg-[#08C1C9] text-[#F2FBFF] rounded-md"
            key={idx}
          >
            {position}
          </div>
        ))}
      </div>
    </div>
  );
};

export default SearchDisplay;
