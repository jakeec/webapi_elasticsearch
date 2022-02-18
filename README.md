# Search API

Taking what was learned from previous Elasticsearch demo and applying to a webapi app using Docker.

## Prerequisites

- Install Docker
- Install Elasticsearch

## Board

| TODO | DOING                      | DONE                      |
| ---- | -------------------------- | ------------------------- |
|      |                            | "Hello, World" app        |
|      |                            | Initial Docker setup      |
|      |                            | Search controller         |
|      | Elasticsearch Docker setup |                           |
|      |                            | Elasticsearch integration |

## Notes

- [Elasticsearch Docker documentation](https://www.elastic.co/guide/en/elasticsearch/reference/current/docker.html)
- Hits vs Documents - What's the difference?
- ISearchResponse.Suggest - Supposedly provides some dictionary type contruct with what are presumably suggestions for terms in the dataset with low edit distance from user-provided search term. Find a way to serialise this nicely for the frontend and it could be useful.
