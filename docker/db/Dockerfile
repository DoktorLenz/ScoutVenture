# Image containing a postgres server containing keycloak data prepared for movie-db.

FROM postgres:15.7@sha256:170ef27196b0b97d21a698b9d3aa2257a29e9fce4b5618f7282d7e6d0daded92

# Default postgres-port
EXPOSE 5432

ENV POSTGRES_PASSWORD=password

# Copy Init-script and keycloak-dump
COPY init-scripts/database-initialization.sh /docker-entrypoint-initdb.d/database-initialization.sh
