
# POC_ORM_NetCore

Repository with Examples and Performance tests with .NET Core 2.0 and EntityFramework Core, Dapper, NHibernate and ADO with AutoMapper.

Versions Used:
- Dapper 1.50.4
- EntityFramework Core 2.0.2
- NHibernate 5.1.1

# Test Example: 

**Initializing Tests**

**Test Number 1: SELECT * FROM Messages**

## Dapper

- 1999ms

- 2115ms

- 2262ms

- 2071ms

- 2026ms

**Average**: 2094.6

## ADO

- 2344ms

- 2046ms

- 2106ms

- 1799ms

- 1978ms

**Average**: 2054.6

## EntityFramework

- 6841ms

- 6179ms

- 6051ms

- 7061ms

- 5716ms

**Average**: 6369.6
