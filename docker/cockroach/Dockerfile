FROM cockroachdb/cockroach:v24.1.1@sha256:60ccb54902286f6c1e71edeb3a8f28ecba6bcc14eb9998ed48061e0a94346919

ADD ./docker/cockroach/data.tar.gz /cockroach

CMD ["start-single-node", "--insecure"]