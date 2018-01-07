using UnityEngine;
using System.Collections;
using System.Data;
using System.Data.SqlClient;

public class Connectivity : MonoBehaviour {
	private string connectionString;
	// Use this for initialization
	void Start () {
		Debug.Log("Connecting to database...");
		connectionString = @"Data Source = 27.147.182.102;
							user id = su;
							password = sa2009;
							Initial Catalog = KidsPuzzle;";

		SqlConnection dbConnection = new SqlConnection (connectionString);

		try{
			dbConnection.Open();
			Debug.Log("Connected to database");

            // Create the command
            SqlCommand command = new SqlCommand("SELECT * FROM tbl_World", dbConnection);
            // Add the parameters.
            command.Parameters.Add(new SqlParameter("0", 1));

            /* Get the rows and display on the screen! 
             * This section of the code has the basic code
             * that will display the content from the Database Table
             * on the screen using an SqlDataReader. */

            using (SqlDataReader reader = command.ExecuteReader())
            {
                Debug.Log("FirstColumn\tSecond Column\t\tThird Column\t\tForth Column\t");
                while (reader.Read())
                {
                    Debug.Log(System.String.Format("{0} \t | {1} \t | {2} \t | {3}",
                        reader[0], reader[1], reader[2], reader[3]));
                }
            }
            Debug.Log("Data displayed! Now press enter to move to the next section!");
        }
		catch(SqlException _exception){
			Debug.LogWarning(_exception.ToString());
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
