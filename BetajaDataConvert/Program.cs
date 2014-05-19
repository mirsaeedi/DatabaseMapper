
using BetajaDataConvert.CooperativeCompanyConversion;
using BetajaDataConvert.CooperativeCompanyConversion.Model;
using BetajaDataConvert.DatabaseDriver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BetajaDataConvert
{
    class Program
    {
        static void Main(string[] args)
        {
            // ******** Base Tables *********

            //ConvertorPeojectMembershipStatus.Convert();
            //ConvertorMilitaryRank.Convert();
            //ConvertorMarriageStatus.Convert();
            //ConvertorAssetCode.Convert();
            //ConvertorSeperateCode.Convert();
            //ConvertorOrganizationalHouse.Convert();
            //ConvertorApplicantSex.Convert();
            //ConvertorApplicantIdentity.Convert();
            //ConvertorMilitaryForce.Convert();
            //ConvertorMilitaryUnit.Convert();
            //ConvertorVaziatKhedmati.Convert();
            //ConvertorLoanReason.Convert();

            //ApplicantIdentityMiss.FindNonDuplicate();
            //SeperatedCodeMiss.FindNonDuplicate();
            //MilitaryUnitMiss.FindNonDuplicate();
            //RankMiss.FindNonDuplicate();
            //AssetCodeMiss.FindNonDuplicate();
            //ApplicantServiceStatusMiss.FindNonDuplicate();


            // ******** Tables *********

            //ConvertorApplicantInfo.Convert();
            //ConvertorApplicantNationlIdFromBagheri.Convert();
            //ConvertorApplicantNationlIdFromUnderSupport.Convert();
            //ConvertorUnderSupport.Convert();
            
            //ConvertorApplicantLoanInfoTblPrsVam.Convert();
            //ConvertorApplicantLoanInfoTblVam.Convert();

            //ConvertorProject.Convert();
            //ConvertorProjectMembership.Convert();
            
            //ConvertorFrontline.Convert();
                    
            //ConvertorCode62Info.Convert();
            //ConvertorDissuasion62Info.Convert();
            
            ConvertorTables.Convert();
           
            Console.WriteLine("THE END");
            Console.ReadLine();
            
        }
    }
}
