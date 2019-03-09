using System.Collections.Generic;
using System.Linq;
using AspNet.Docker.Integration.Repository;
using AspNet.Docker.Integration.Repository.Models;
using Autofac;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace AspNet.Docker.Integration.UnitTest
{
    [TestClass]
    public class MockUserRepoTest : IoCSupportedTestBase<ContainerFactory>
    {
        [TestInitialize]
        public void TestInit()
        {
            Mock<IUserRepositoy> mockUserRepo = new Mock<IUserRepositoy>();
            mockUserRepo.Setup(r => r.GetAll()).Returns(new List<User>
            {
                new User
                {
                    Id = 1,
                    Name = "Jackson"
                },
                new User
                {
                    Id = 2,
                    Name = "Anne"
                }
            });
            
            UseExternalRegistrar(builder =>
            {
                builder.RegisterInstance(mockUserRepo.Object)
                       .As<IUserRepositoy>()
                       .SingleInstance();
            });
        }
        
        /// <summary>
        /// 清理測試方法
        /// </summary>
        [TestCleanup]
        public void TestMethodCleanUp()
        {
            FinishUsingContainer();
        }
        
        [TestMethod]
        public void TestGetAll()
        {
            IUserRepositoy userRepo = this.Resolve<IUserRepositoy>();
            
            Assert.IsTrue(userRepo.GetAll().FirstOrDefault(u => u.Id == 1)?.Name == "Jackson");
            Assert.IsTrue(userRepo.GetAll().FirstOrDefault(u => u.Id == 2)?.Name == "Anne");
        }
    }
}