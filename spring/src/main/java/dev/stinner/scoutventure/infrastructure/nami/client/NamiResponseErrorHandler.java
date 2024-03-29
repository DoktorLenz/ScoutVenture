package dev.stinner.scoutventure.infrastructure.nami.client;

import dev.stinner.scoutventure.domain.exceptions.nami.NamiException;
import org.springframework.http.client.ClientHttpResponse;
import org.springframework.web.client.ResponseErrorHandler;

import java.io.IOException;

public class NamiResponseErrorHandler implements ResponseErrorHandler {
    @Override
    public boolean hasError(ClientHttpResponse response) throws IOException {
        return response.getStatusCode().isError();
    }

    @Override
    public void handleError(ClientHttpResponse response) throws IOException {
        throw new NamiException("Nami error " + response.getStatusCode().value());
    }
}
