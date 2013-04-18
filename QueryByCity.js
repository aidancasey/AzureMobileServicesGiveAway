function read(query, user, request) {

    if ((request.parameters.city !== null)) {
        query.where({ City: request.parameters.city });
        request.execute();

    }
    else {

        request.execute();
    }
}