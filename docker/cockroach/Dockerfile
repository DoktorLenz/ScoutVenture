FROM cockroachdb/cockroach:v23.2.3@sha256:9faac5ea5ebac35633c560f06464c0c62d18702f1f206d3c26f4f29341ba4a8b

ADD ./docker/cockroach/data.tar.gz /cockroach

CMD ["start-single-node", "--insecure"]