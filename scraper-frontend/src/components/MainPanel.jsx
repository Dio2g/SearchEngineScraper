import { useState } from 'react';
import SearchPanel from './search/SearchPanel';

const MainPanel = () => {
  const [activeTab, setActiveTab] = useState('tab1');

  const tabs = [
    { id: 'tab1', label: 'Search' },
    { id: 'tab2', label: 'History' },
  ];

  const tabContent = {
    tab1: <SearchPanel />,
    tab2: <div className="">History Coming Soon</div>,
  };

  return (
    <div className="border-4 rounded-xl shadow-lg mx-auto mt-10 max-w-2xl overflow-hidden">
      <div className="flex justify-between p-2 border-b-4 bg-[#08C1C9]">
        <h1 className="text-center text-lg p-2 pl-3 font-mono font-extrabold text-[#F2FBFF]">
          Search Engine Scraper
        </h1>

        <div className="flex gap-1">
          {tabs.map((tab) => (
            <button
              key={tab.id}
              className={`p-2 rounded-xl font-light border-2 ${
                activeTab === tab.id
                  ? 'border-[#002621] text-white bg-[#029390]'
                  : 'border-[#005C56] bg-[#50E3FF] hover:bg-[#86E5FF]'
              }`}
              onClick={() => setActiveTab(tab.id)}
            >
              {tab.label}
            </button>
          ))}
        </div>
      </div>
      <div className="py-1 px-3 bg-[#19E8FF]">{tabContent[activeTab]}</div>
    </div>
  );
};

export default MainPanel;
