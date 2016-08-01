# RealEstateCrawler

This project is aimed at scraping data from sites such as Domain.com.au, RealEstate.com and Flatmates.com.au.

# Aim is to:
- Allow full flexibility for adding new websites to the scraper
- Storage of data extracted in mongoDB to be collated for easy access
- Display of housing on a google map view.


# Set up:
(Not tested yet)
- Download either Visual Studio Community Edition (Windows) or Visual Studio Code (macOS/Linux)
- Clone this repo
- `cd RealEstateCrawler/src && code .`
- Install the C# extension in Visual Studio Code
- Set the Runnable Project to RealEstateCrawler.Run in VSCode.
- CTRL+F5
- Should be all set!

# Code Style

```
1. If in C#, curly braces on following line
if (true)
{
  ...
} 
else 
{
  ...
}

1a. If JavaScript/TypeScript curly braces inline
if (true) {
  ...
} else {
  ...
}

2. Use C# 6 snippets
Instead of:
if (thisArray != null && thisArray.Count == 6)
{
  ...
}

Use: 
if (thisArray?.Count == 6)
{
  ...
}

Preferable:
Use C# 6 String Interpolation instead of string.Format

String Interpolation:
$"Size of thisArray is: {thisArray.Count}"

Please remove unused imports/using statements!
```
