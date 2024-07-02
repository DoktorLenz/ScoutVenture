# Image containing a postgres server containing keycloak data prepared for movie-db.

FROM postgres:16.3@sha256:6d2a810d24786a90bf4bf2f4d4ce7a1039696a9269f3293abf3d6c8aa0bab9da

# Default postgres-port
EXPOSE 5432

ENV POSTGRES_PASSWORD=password

# Copy Init-script and keycloak-dump
COPY init-scripts/database-initialization.sh /docker-entrypoint-initdb.d/database-initialization.sh
