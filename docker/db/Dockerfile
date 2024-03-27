# Image containing a postgres server containing keycloak data prepared for movie-db.

FROM postgres:15.6@sha256:ea40a028dd42740d6cff34135ff6b3119ff7ce0ed60120d992216456b5987fe7

# Default postgres-port
EXPOSE 5432

ENV POSTGRES_PASSWORD=password

# Copy Init-script and keycloak-dump
COPY init-scripts/database-initialization.sh /docker-entrypoint-initdb.d/database-initialization.sh
