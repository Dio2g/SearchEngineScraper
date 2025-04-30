# SearchEngineScraper
WARNING: Scraping Bing may be against there ToS, to find out which parts of the site are allowed to be scraped check https://www.bing.com/robots.txt. 

IMPORTANT NOTE: As of January 2025 Google now requires JavaScript to submit search queries  (https://www.techspot.com/news/106421-javascript-now-mandatory-google-search-google-confirms.html) so I don't believe its possible to use HttpClient to do this (gven the project scope). If the use of 3rd party libraries were allowed id recommend using puppeteer or selenium to scrape the site to have js and bypass bot detections. Instead of google I used Bing in this project.

Prerequisites: node.js, .net 8

Steps to run project:

Ensure you have the latest node.js and .net 8 installed

Run run-backend.bat in project root.
Run run-frontend.bat in project root.

Navigate to: http://localhost:5173/ (Or displayed address in front-end console)

(Alternative instructions if you dont want to run .bat files:

Ensure you have the latest node.js and .net 8 installed

Open  cmd and navigate to  S'craperApi\ScraperApi'}
Run commands:
call dotnet restore
call dotnet build
call dotnet run

Open anothercmd and navigate to 'SearchEngineScraper\scraper-frontend'
Run Commands: 
npm install
npm run dev

Navigate to: http://localhost:5173/ (Or displayed address in front-end console))

Screenshots:

![image](https://github.com/user-attachments/assets/c40e8108-29ca-4c0b-a952-45ae85766b67)

![image](https://github.com/user-attachments/assets/2e796cbf-c1e5-41c0-8143-7d90c496f142)

Things to note:
There are some features that havent been fully implemented yet like the Yahoo scraper so some parts of the app may not work.





