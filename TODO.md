# To Do List for RealEstateCrawler

# Crawler
    - Constantly crawling in the background for each suburb]
    - How do we get the suburb list?
        - Based on popularity?
        - Just get all from the State/Country?
    - Crawler service per Site?
    - Crawler service per State?
    - What can we do to make this as fast as possible?

# Database List
    - Store each as a hashId (Number, Streetname, Postcode)
    - Needs a Site Table, has foreign key relationship on the hashId
    - Site(*)->(1)property
    - What database to use? 
        - https://www.arangodb.com/wp-content/uploads/2015/09/chart_v2071.png
        - Graph database? 
            - Neo4j
                - Graphical representation as a Graph
                - Site -> Property <- State
        - MongoDB? 
            - Very large data sets
            - Fast to return all JSON Data
            - Low memory footprint
        - Standard SQL (probs not)

# Merge Service Rules
    - Same Number, Street Name, State, (POSTCODE - Important)

# Website
    - Google Maps API Hookup? Maybe other mapping software?
    - Plot properties in each suburb
    - Able to identify each suburb in the viewport, send back to the crawler to plot everything?
        - Crawl if we cant get stuff from the DB. 

