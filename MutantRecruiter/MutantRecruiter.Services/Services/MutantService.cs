using Microsoft.Extensions.Configuration;
using MutantRecruiter.Services.Contract;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MutantRecruiter.Services.Services
{
    public class MutantService : IMutantService
    {
        private readonly IConfiguration _config;
        private readonly IQueueService<Human> _queueService;
        private string[,] _dnaChain;
        public MutantService(IConfiguration config, IQueueService<Human> queueService)
        {
            _config = config;
            _queueService = queueService;
        }

        /// <summary>
        /// Main method to evaluate the dna chain
        /// </summary>
        /// <param name="human"></param>
        /// <returns>True: Is mutant, False: Not mutant</returns>

        public async Task<bool> IsMutant(Human human)
        {
            if (IsValidDNA(human.DNA))
            {
                _dnaChain = SeparateChain(human.DNA);
                human.IsMutant = await ValidateDNAMutant();
                SaveHumanInfo(human);
                return human.IsMutant;
            }
            else
                throw new Exception();
        }

        /// <summary>
        /// method to determine if the dna chain is made correctly
        /// </summary>
        /// <param name="dna">DNA Chain</param>
        /// <returns>True: Correct dna chain, False: Incorrect dna chain</returns>
        private bool IsValidDNA(string[] dna)
        {
            string pattern = _config.GetValue<string>("RegexDNA");
            if (dna.Length > 3)
            {
                foreach (var item in dna)
                {
                    if (item.Length != dna.Length || Regex.IsMatch(item, pattern))
                        return false;
                }
            }
            else
                return false;
            return true;
        }

        /// <summary>
        /// Method to separate all the components of the chain of and
        /// </summary>
        /// <param name="dna"></param>
        /// <returns>DNA chain in the matrix</returns>
        private string[,] SeparateChain(string[] dna)
        {
            string[,] dnaChain = new string[dna.Length, dna.Length];
            for (int i = 0; i < dna.Length; i++)
            {
                int j = 0;
                foreach (char c in dna[i])
                {
                    dnaChain[i, j] = c.ToString();
                    j++;
                }
            }
            return dnaChain;
        }

        /// <summary>
        /// Method to evaluate the DNA chain
        /// </summary>
        /// <returns>True: Is mutant, False: Not mutant</returns>
        private async Task<bool> ValidateDNAMutant()
        {
            int countChain = 0;
            int n = _dnaChain.GetLength(0); // Size matrix's rows and columns;
            for (int i = 0; i < n; i++)
            {
                for (int j = 0; j < n; j++)
                {
                    if (i < n - 3)
                        countChain = countChain + await validateMutantChain(i, j, 1, 0, 0, _dnaChain[i, j]); //Vertical
                    if (j < n - 3)
                        countChain = countChain + await validateMutantChain(i, j, 0, 1, 0, _dnaChain[i, j]); // Horizontal
                    if (j < n - 3 && i < n - 3)
                        countChain = countChain + await validateMutantChain(i, j, 1, 1, 0, _dnaChain[i, j]); // Diagonal
                }
                if (countChain > 1)
                    return true;
            }
            return false;

        }

        /// <summary>
        /// Method to evaluate the DNA chain
        /// </summary>
        /// <param name="rowInit">row position in the matrix</param>
        /// <param name="columnInit">column position in the matrix</param>
        /// <param name="rowMove">move in row direction</param>
        /// <param name="columnMove">move in column direction</param>
        /// <param name="quantity">quantity of equal letters</param>
        /// <param name="letter">letter to evaluate</param>
        /// <returns></returns>
        private async Task<int> validateMutantChain(int rowInit, int columnInit, int rowMove, int columnMove, int quantity, string letter)
        {
            if (_dnaChain[rowInit + rowMove, columnInit + columnMove] == letter)
            {
                quantity++;
                if (quantity == 3)
                    return 1;
                else
                    return await validateMutantChain(rowInit + rowMove, columnInit + columnMove, rowMove, columnMove, quantity, letter);
            }
            return 0;
        }

        private async void SaveHumanInfo(Human human)
        {
            _queueService.QueueStack(human);
        }
    }
}
