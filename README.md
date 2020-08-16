# ClickCounter
The application keeps track of clicks on media results fetched from iTunes Search API

iTunes Lookup
The application acts as a middleman for searching Apple iTunes. The application keeps track of
user’s clicks and the clicks can be seen and later analyzed.
The application follows MVC architecture and is built using:-
1. C#
2. ASP .NET v4.7.2
3. Newtonsoft-Json v12.0.3
4. MVC v5.2.7.0
5. jQuery v3.4.1
6. Materialize v1.0.0

## Models
1. SearchItem - This is the smallest grain of this application. It has attributes that are part of
one result entry which is returned by iTunes API.
2. SearchResult - This is a singleton class that consists of a dictionary that contains all the
SearItem which have been clicked by the user. TrackId is the key for SearchItem lookup
in this dictionary.
3. JsonResults - This is the envelope class that is used to store the dump from iTunes API
directly. It has two keys - ResultCount and Results (lits of results).

## Views
All views are in Home directory and belong to HomeController. Views use MaterializeCSS
heavily and some custom CSS and jQuery, for their design. Data sharing between Controller
and Views are handled by chtml code.
1. Index - The home page of the application.
2. About - Brief about the application.
3. Contact - Contact of creator and support.
4. Exception - A generic view to display any exception or error.
5. Search - View displaying the search results.
6. CheckClicks - A view to display each result’s clicks.

## Controller
HomeController - It is the only controller. It has the following methods: -
1. Index - to render Index view
2. About - to render About view
3. Contact - to render Contct View
4. ClickSearch - takes trackId of content as input and appends the SearchItem object in
SearchResult’s result dictionary. This method calls iTunes’s Lookup API in case the
SearchItem is absent from the result dictionary. Returns a boolean value.
5. Search - calls the iTune’s search API and build’s JsonResults object using Newtonsoft’s
Deserializer. JsonReuslts’s Result list is sent to the view for rendering.
6. CheckClicks - uses SearchResult’s singleton object and renders it in a view.
