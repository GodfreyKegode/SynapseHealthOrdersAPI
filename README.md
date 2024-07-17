# SynapseHealthOrderMonitorAPI
Additional ToDos to make this more production ready:
- Implement Authentication & Authorization
- Implement a HttpResponseMessage middleware to wrap my outgoing API messages with a HTTP status success, status code and user friendly message as part of the json response body
- Add DTOs so I'm not exposing whole entities to the endpoints
- Add validation Helper e.g. Fluent Validation on DTOs to validate incoming properties of requests
- Refine configuration files for various env. & dependency endpoints
