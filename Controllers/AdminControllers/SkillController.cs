using APIWeb1.Interfaces;
using APIWeb1.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace APIWeb1.Controllers.AdminControllers
{
    [Authorize(Roles = "Admin")]
    public class SkillController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        public SkillController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IActionResult Index()
        {
            List<Skill> skillList = _unitOfWork.SkillRepo.GetAll().ToList();
            return View(skillList);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Create(Skill skill)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.SkillRepo.Add(skill);
                _unitOfWork.Save();
                TempData["success"] = "Skill created successfully";
                return RedirectToAction("Index", "Skill");
            }
            return View();
        }

        public IActionResult Update(int id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }
            Skill? skill = _unitOfWork.SkillRepo.Get(x => x.Id == id);
            if (skill == null)
            {
                return NotFound();
            }
            return View(skill);
        }
        [HttpPost]
        public IActionResult Update(Skill skill)
        {
            if (ModelState.IsValid)
            {
                _unitOfWork.SkillRepo.Update(skill);
                _unitOfWork.Save();
                TempData["success"] = "Skill updated successfully";

                return RedirectToAction("Index", "Skill");
            }
            return View();
        }

        /*        public IActionResult Delete(int id)
                {
                    if (id == null || id == 0)
                    {
                        return NotFound();
                    }
                    Skill? skill = _unitOfWork.SkillRepo.Get(x => x.Id == id);
                    if (skill == null)
                    {
                        return NotFound();
                    }
                    return View(skill);
                }*/
        /*        [HttpPost, ActionName("Delete")]
                public IActionResult DeletePOST(int id)
                {
                    Skill? skill = _unitOfWork.SkillRepo.Get(x => x.Id == id);
                    if (skill == null)
                    {
                        return NotFound();
                    }
                    _unitOfWork.SkillRepo.Remove(skill);
                    _unitOfWork.Save();
                    TempData["success"] = "Skill deleted successfully";
                    return RedirectToAction("Index", "JobSkill");
                }*/


        #region API CALLS
        [HttpGet]
        public IActionResult GetAll()
        {
            List<Skill> skillListObj = _unitOfWork.SkillRepo.GetListTrue(x => x.IsDeleted == false).ToList();
            return Json(new { data = skillListObj });
        }

        public IActionResult Delete(int id)
        {
            var skillDelete = _unitOfWork.SkillRepo.Get(x => x.Id == id);
            if (skillDelete == null)
            {
                return Json(new { success = false, message = "Error while deleting" });
            }
            skillDelete.IsDeleted = true;
            _unitOfWork.SkillRepo.Update(skillDelete);
            _unitOfWork.Save();
            return Json(new { success = true, message = "Delete Successfully" });
        }
        #endregion
    }
}
