# Image containing a postgres server containing keycloak data prepared for movie-db.

FROM postgres:16.2@sha256:6b841c8f6a819884207402f1209a8116844365df15fca8cf556fc54a24c70800

# Default postgres-port
EXPOSE 5432

ENV POSTGRES_PASSWORD=password

# Copy Init-script and keycloak-dump
COPY init-scripts/database-initialization.sh /docker-entrypoint-initdb.d/database-initialization.sh
