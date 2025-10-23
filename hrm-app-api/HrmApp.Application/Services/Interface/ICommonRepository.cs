using HrmApp.Shared.DTOs;

namespace HrmApp.Application.Services.Interface;

public interface ICommonRepository
{
    Task<List<CommonViewModel>> GetAllDepartment(int idClient);

    Task<List<CommonViewModel>> GetAllDesignation(int idClient);

    Task<List<CommonViewModel>> GetAllEducationExamination(int idClient);

    Task<List<CommonViewModel>> GetAllEducationLevel(int idClient);

    Task<List<CommonViewModel>> GetAllEducationResult(int idClient);

    Task<List<CommonViewModel>> GetAllEmployeeType(int idClient);

    Task<List<CommonViewModel>> GetAllGender(int idClient);

    Task<List<CommonViewModel>> GetAllJobType(int idClient);

    Task<List<CommonViewModel>> GetAllMaritalStatus(int idClient);

    Task<List<CommonViewModel>> GetAllRelationship(int idClient);

    Task<List<CommonViewModel>> GetAllReligion(int idClient);

    Task<List<CommonViewModel>> GetAllSection(int idClient);

    Task<List<CommonViewModel>> GetAllWeekOff(int idClient);
}