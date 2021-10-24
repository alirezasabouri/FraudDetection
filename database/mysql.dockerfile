FROM mysql:5.7.25

ARG FLYWAY_VERSION="5.2.4"
ARG FLYWAY_CHECKSUM="c4b5f7ae3ec41221f78a126b0a1637942484fa3f"

ENV DATABASE_HOST="localhost"
ENV DATABASE_PORT="3306"

# curl is needed for installation of Flyway.
# bash is needed for the wrapper script around flyway.
# netcat is needed to validate whether the database is accepting connections.
RUN apt-get -y update \
    && apt-get -y install curl bash netcat dos2unix \
    && rm -rf /var/lib/apt/lists/*

RUN mkdir -p /opt/flyway \
    && curl -s -o /tmp/flyway.tar.gz https://repo1.maven.org/maven2/org/flywaydb/flyway-commandline/${FLYWAY_VERSION}/flyway-commandline-${FLYWAY_VERSION}-linux-x64.tar.gz \
    && echo "${FLYWAY_CHECKSUM} /tmp/flyway.tar.gz" | sha1sum -c --quiet - \
    && tar -xz -f /tmp/flyway.tar.gz --strip-components=1 -C /opt/flyway \
    && rm /tmp/flyway.tar.gz

ENV PATH=/opt/flyway:$PATH

COPY ./run-flyway.sh /
WORKDIR /database

# runs the flyway migrations in the background and starts the mysql server.
# the run-flyway.sh script waits for the database to be started.
CMD dos2unix /run-flyway.sh && /run-flyway.sh& docker-entrypoint.sh mysqld