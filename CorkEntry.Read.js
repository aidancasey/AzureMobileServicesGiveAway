function read(query, user, request) {

    mssql.query("select * from entry where city = 'Cork'", {
        success: function (results) {
            request.respond(statusCodes.OK, results);
        }
    }
    );
}
