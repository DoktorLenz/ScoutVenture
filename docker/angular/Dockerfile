FROM nginx:1.25.4-alpine@sha256:31bad00311cb5eeb8a6648beadcf67277a175da89989f14727420a80e2e76742

ENV CONFIG=e2e

# Copy the build output to replace the default nginx contents.
COPY ./angular/dist/scoutventure-spa /usr/share/nginx/html
# Copy custom config for nginx
COPY ./docker/angular/frontend.conf /etc/nginx/conf.d/default.conf

# Expose port 80
EXPOSE 80
