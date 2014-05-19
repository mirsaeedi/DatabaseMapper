using BetajaDataConvert.Conversion.Model;
using BetajaDataConvert.DatabaseDriver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetajaDataConvert
{
    class ConvertorTables
    {
        public static void Convert()
        {
            PostgreSqlDriver dbWriter=new PostgreSqlDriver("localhost","Maskan","maskan_admin","12345");
            string fileName = "";
            /*
            fileName = @"convertScripts\Applicants\dbo.applicantinfo-1.sql";

            dbWriter.ExecuteQueryFromFile(fileName);
            
            fileName = @"convertScripts\Applicants\dbo.applicantinfo-2.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

           fileName = @"convertScripts\Applicants\dbo.applicantinfo-3.sql";
           dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\Applicants\dbo.applicantinfo-4.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

           fileName = @"convertScripts\Applicants\dbo.applicantinfo-5.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\Applicants\dbo.applicantinfo-6.sql";
           dbWriter.ExecuteQueryFromFile(fileName);
            
            fileName = @"convertScripts\Applicants\dbo.applicantinfo-7.sql";
           dbWriter.ExecuteQueryFromFile(fileName);
            
           fileName = @"convertScripts\Applicants\dbo.applicantinfo-8.sql";
            dbWriter.ExecuteQueryFromFile(fileName);
            
           fileName = @"convertScripts\Applicants\dbo.applicantinfo-9.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\Applicants\dbo.applicantinfo-10.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

           fileName = @"convertScripts\Applicants\dbo.applicantinfo-11.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\Applicants\dbo.applicantinfo-12.sql";
           dbWriter.ExecuteQueryFromFile(fileName);

           fileName = @"convertScripts\Applicants\dbo.applicantinfo-13.sql";
           dbWriter.ExecuteQueryFromFile(fileName);
            
           
           fileName = @"convertScripts\Code62\Code62Info\dbo.code62info-1.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\Code62\Code62Info\dbo.code62info-2.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\Code62\Code62Info\dbo.code62info-3.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\Code62\Code62Info\dbo.code62info-4.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\Code62\Code62Info\dbo.code62info-5.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\Code62\Code62Info\dbo.code62info-6.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\Code62\Code62Info\dbo.code62info-7.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\Code62\Code62Info\dbo.code62info-8.sql";
            dbWriter.ExecuteQueryFromFile(fileName);
            
            fileName = @"convertScripts\Code62\Dissuasion62Info\dbo.dissuasion62info-1.sql";
            dbWriter.ExecuteQueryFromFile(fileName);
              
            fileName = @"convertScripts\Code62\Dissuasion62Info\dbo.dissuasion62info-2.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\Code62\Dissuasion62Info\dbo.dissuasion62info-3.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\Code62\Dissuasion62Info\dbo.dissuasion62info-4.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\Code62\Dissuasion62Info\dbo.dissuasion62info-5.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\Code62\Dissuasion62Info\dbo.dissuasion62info-6.sql";
            dbWriter.ExecuteQueryFromFile(fileName);
            
            
            
            fileName = @"convertScripts\Loan\dbo.applicantloaninfo-1.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\Loan\dbo.applicantloaninfo-2.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\Loan\dbo.applicantloaninfo-3.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            

                       fileName = @"convertScripts\Project\dbo.project-1.sql";
                       dbWriter.ExecuteQueryFromFile(fileName);   
           
                       fileName= @"convertScripts\ProjectMembership\dbo.projectmembership-1.sql";
                       dbWriter.ExecuteQueryFromFile(fileName);

                       fileName = @"convertScripts\ProjectMembership\dbo.projectmembership-2.sql";
                       dbWriter.ExecuteQueryFromFile(fileName); 
             
                      
            
            */
            
            fileName = @"convertScripts\Frontline\dbo.frontline-1.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\Frontline\dbo.frontline-2.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\Frontline\dbo.frontline-3.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\Frontline\dbo.frontline-4.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\Frontline\dbo.frontline-5.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\Frontline\dbo.frontline-6.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\Frontline\dbo.frontline-7.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\Frontline\dbo.frontline-8.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\Frontline\dbo.frontline-9.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\Frontline\dbo.frontline-10.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\Frontline\dbo.frontline-11.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\Frontline\dbo.frontline-12.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\Frontline\dbo.frontline-13.sql";
            dbWriter.ExecuteQueryFromFile(fileName);
           
            

            
            fileName = @"convertScripts\UnderSupportInfo\dbo.undersupportinfo-1.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\UnderSupportInfo\dbo.undersupportinfo-2.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\UnderSupportInfo\dbo.undersupportinfo-3.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\UnderSupportInfo\dbo.undersupportinfo-4.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\UnderSupportInfo\dbo.undersupportinfo-5.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\UnderSupportInfo\dbo.undersupportinfo-6.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\UnderSupportInfo\dbo.undersupportinfo-7.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\UnderSupportInfo\dbo.undersupportinfo-8.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\UnderSupportInfo\dbo.undersupportinfo-9.sql";
            dbWriter.ExecuteQueryFromFile(fileName);
            
             
            fileName = @"convertScripts\UpdateNationalIdFromUnderSupport\dbo.applicantinfo-1.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\UpdateNationalIdFromUnderSupport\dbo.applicantinfo-2.sql";
            dbWriter.ExecuteQueryFromFile(fileName);

            fileName = @"convertScripts\UpdateNationalIdFromUnderSupport\dbo.applicantinfo-3.sql";
            dbWriter.ExecuteQueryFromFile(fileName);
              
           
           
            fileName = @"convertScripts\UpdateNationalIdFromBagheri\dbo.applicantinfo-1.sql";
                dbWriter.ExecuteQueryFromFile(fileName);

                fileName = @"convertScripts\UpdateNationalIdFromBagheri\dbo.applicantinfo-2.sql";
                dbWriter.ExecuteQueryFromFile(fileName);

                fileName = @"convertScripts\UpdateNationalIdFromBagheri\dbo.applicantinfo-3.sql";
                dbWriter.ExecuteQueryFromFile(fileName);

                fileName = @"convertScripts\UpdateNationalIdFromBagheri\dbo.applicantinfo-4.sql";
                dbWriter.ExecuteQueryFromFile(fileName);

                fileName = @"convertScripts\UpdateNationalIdFromBagheri\dbo.applicantinfo-5.sql";
                dbWriter.ExecuteQueryFromFile(fileName);

                fileName = @"convertScripts\UpdateNationalIdFromBagheri\dbo.applicantinfo-6.sql";
                dbWriter.ExecuteQueryFromFile(fileName);

                fileName = @"convertScripts\UpdateNationalIdFromBagheri\dbo.applicantinfo-7.sql";
                dbWriter.ExecuteQueryFromFile(fileName);

                fileName = @"convertScripts\UpdateNationalIdFromBagheri\dbo.applicantinfo-8.sql";
                dbWriter.ExecuteQueryFromFile(fileName);

                fileName = @"convertScripts\UpdateNationalIdFromBagheri\dbo.applicantinfo-9.sql";
                dbWriter.ExecuteQueryFromFile(fileName);

                fileName = @"convertScripts\UpdateNationalIdFromBagheri\dbo.applicantinfo-10.sql";
                dbWriter.ExecuteQueryFromFile(fileName);

                fileName = @"convertScripts\UpdateNationalIdFromBagheri\dbo.applicantinfo-11.sql";
                dbWriter.ExecuteQueryFromFile(fileName);

                fileName = @"convertScripts\UpdateNationalIdFromBagheri\dbo.applicantinfo-12.sql";
                dbWriter.ExecuteQueryFromFile(fileName);

                fileName = @"convertScripts\UpdateNationalIdFromBagheri\dbo.applicantinfo-13.sql";
                dbWriter.ExecuteQueryFromFile(fileName);
             
              
        }
    }
}
