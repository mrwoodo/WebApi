using System;

namespace WebApiTest
{
    public class SearchRequest
    {
        public string Surname { get; set; }
        public string GivenNames { get; set; }
        public DateTime DateOfBirth { get; set; }
        public string StreetName { get; set; }

        public SearchRequest()
        {
            Surname = "Smith";
            GivenNames = "Chris";
            DateOfBirth = new DateTime(1980, 1, 1);
            StreetName = "Eastern Valley Way";
        }

        public override string ToString()
        {
            var surname = System.Web.HttpUtility.UrlEncode(Surname);
            var givenNames = System.Web.HttpUtility.UrlEncode(GivenNames);
            var dateOfBirth = System.Web.HttpUtility.UrlEncode(DateOfBirth.ToString("dd/MM/yyyy"));
            var streetName = System.Web.HttpUtility.UrlEncode(StreetName);

            var queryString = $"{nameof(Surname)}={surname}&" +
                $"{nameof(GivenNames)}={givenNames}&" +
                $"{nameof(DateOfBirth)}={dateOfBirth}&" +
                $"{nameof(StreetName)}={streetName}";

            return queryString;
        }
    }
}
