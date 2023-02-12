A class intended to be used to easily construct and update urls, especially url parameters. Very helpful for building out API clients and data scraping

Serializes complex objects and appends properties to the url query. Use the HttpQueryPropertyAttribute to define how properties are serialized.

The purpose of this library is specifically to ease the process of modifying query strings for API's. 

Usage examples

```
UrlBuilder builder = new UrlBuilder("https://www.google.com/search");

builder.AddParameter("q", "My Search Term");

Console.WriteLine(builder.ToString()); //https://www.google.com/search?q=My%20Search%20Term

```

```
UrlBuilder builder = new UrlBuilder("https://api.rest.com/users/search");

builder.AddParameter(new MySearchClass() {
	Page = 1,
	Count = 20,
	Query = "Admin"
});

Console.WriteLine(builder.ToString()); //https://api.rest.com/users/search?Page=1&Count=20&Query=Admin

```

Additional information will be provided on full release