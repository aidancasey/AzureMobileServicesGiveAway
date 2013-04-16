@model IEnumerable<$rootnamespace$.Models.UserProfile>
@{
    
    ViewBag.Title = "Users";
    var roles = ((string[])ViewBag.Roles).OrderBy(x => x);
}

<h2>Users</h2>


<table>
    <thead>
        <tr>
            <th>Username

            </th>
            @foreach (var role in roles)
            {
                <th>@role
                </th>
            }

        </tr>


    </thead>
    <tbody>
        @foreach (var user in Model)
        {
            <tr>
                <td>

                    @user.UserName
                </td>

                @foreach (var role in roles)
                {
                    <td>
                        @using (Html.BeginForm("UserToRole", "Main"))
                        {
                            <input type="hidden" value="@user.UserName" name="username" />
                            <input type="hidden" value="@role" name="rolename" />
                            <input type="checkbox" name="ischecked" onclick="this.form.submit();" value="true" checked="@System.Web.Security.Roles.IsUserInRole(@user.UserName,role)" />
                            
                        }


                    </td>
                }

            </tr>  
        
        }
    </tbody>
</table>

<p>
    <a href="~/SimpleMembershipAdministration/Main/Roles">Roles</a>
</p>
