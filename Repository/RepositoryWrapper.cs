using Contracts;
using Entities;
using Entities.Helpers;
using Entities.Models;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private RepositoryContext _repoContext;
        private IOwnerRepository _owner;
        private IAccountRepository _account;
        private IProductRepository _product;
        private IUserRepository _user;
        private IVotingRepository _voting;
        private ICategoryRepository _category;
        private ISortHelper<Owner> _ownerSortHelper;
        private ISortHelper<Account> _accountSortHelper;
        private readonly ISortHelper<Product> _productSortHelper;
        private readonly ISortHelper<Voting> _votingSortHelper;
        private readonly ISortHelper<Category> _categorySortHelper;
        private IDataShaper<Owner> _ownerDataShaper;
        private IDataShaper<Account> _accountDataShaper;
        private readonly IDataShaper<Product> _productDataShaper;
        private readonly IDataShaper<Voting> _votingDataShaper;
        private readonly IDataShaper<Category> _categoryDataShaper;

        public IOwnerRepository Owner
        {
            get
            {
                if (_owner == null)
                {
                    _owner = new OwnerRepository(_repoContext, _ownerSortHelper, _ownerDataShaper);
                }
                return _owner;
            }
        }
        public IAccountRepository Account
        {
            get
            {
                if (_account == null)
                {
                    _account = new AccountRepository(_repoContext, _accountSortHelper, _accountDataShaper);
                }
                return _account;
            }
        }
        public IProductRepository Product
        {
            get
            {
                if (_product == null)
                {
                    _product = new ProductRepository(_repoContext, _productSortHelper, _productDataShaper);
                }
                return _product;
            }
        }

        public IUserRepository User
        {
            get
            {
                if (_user == null)
                {
                    _user = new UserRepository(_repoContext);
                }
                return _user;
            }
        }

        public IVotingRepository Voting
        {
            get
            {
                if (_voting == null)
                {
                    _voting = new VotingRepository(_repoContext, _votingSortHelper, _votingDataShaper);
                }
                return _voting;
            }
        }

        public ICategoryRepository Category
        {
            get
            {
                if (_category == null)
                {
                    _category = new CategoryRepository(_repoContext, _categorySortHelper, _categoryDataShaper);
                }
                return _category;
            }
        }

        public RepositoryWrapper(RepositoryContext repositoryContext,
            ISortHelper<Owner> ownerSortHelper,
            ISortHelper<Account> accountSortHelper,
            ISortHelper<Product> productSortHelper,
            ISortHelper<Voting> votingSortHelper,
            ISortHelper<Category> categorySortHelper,
            IDataShaper<Owner> ownerDataShaper,
            IDataShaper<Account> accountDataShaper,
            IDataShaper<Product> productDataShaper,
            IDataShaper<Voting> votingDataShaper,
            IDataShaper<Category> categoryDataShaper
            )
        {
            _repoContext = repositoryContext;
            _ownerSortHelper = ownerSortHelper;
            _accountSortHelper = accountSortHelper;
            _productSortHelper = productSortHelper;
            _votingSortHelper = votingSortHelper;
            _categorySortHelper = categorySortHelper;
            _ownerDataShaper = ownerDataShaper;
            _accountDataShaper = accountDataShaper;
            _productDataShaper = productDataShaper;
            _votingDataShaper = votingDataShaper;
            _categoryDataShaper = categoryDataShaper;
        }
        public void Save()
        {
            _repoContext.SaveChanges();
        }
    }
}