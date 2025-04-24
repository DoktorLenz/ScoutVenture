FROM nginx:1.28.0-alpine@sha256:aed99734248e851764f1f2146835ecad42b5f994081fa6631cc5d79240891ec9

ENV CONFIG=e2e

# Copy the build output to replace the default nginx contents.
COPY ./angular/dist/scoutventure-spa /usr/share/nginx/html
# Copy custom config for nginx
COPY ./docker/angular/frontend.conf /etc/nginx/conf.d/default.conf

# Expose port 80
EXPOSE 80
