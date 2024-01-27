package dev.stinner.scoutventure.infrastructure.jpa;

import org.springframework.boot.autoconfigure.jdbc.DataSourceProperties;
import org.springframework.boot.context.properties.ConfigurationProperties;
import org.springframework.context.annotation.Bean;
import org.springframework.context.annotation.Configuration;
import org.springframework.context.annotation.Primary;

import javax.sql.DataSource;

@Configuration
public class ScoutVentureDatasourceConfiguration {

    @Bean
    @ConfigurationProperties("spring.datasource.scoutventure")
    public DataSourceProperties scoutVentureDataSourceProperties() {
        return new DataSourceProperties();
    }

    @Bean
    @Primary
    public DataSource scoutVentureDataSource() {
        return scoutVentureDataSourceProperties().initializeDataSourceBuilder().build();
    }

}
