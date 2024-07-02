FROM cockroachdb/cockroach:v23.2.6@sha256:20ef1af8b41e763f1e7521e2d85ca35b1594143ba00cfd295aacd068270c156a

ADD ./docker/cockroach/data.tar.gz /cockroach

CMD ["start-single-node", "--insecure"]