using UnityEngine;

namespace Exercise
{
    public class GeneA : GeneAbstract<int>
    {

        /**
         * @return calculates and returns the fitness value of the individual
         */
        public override float CalculateFitness()
        {
            string phenotype = GetPhenotype();
            float fitness = 0;

            foreach (var c in phenotype)
            {
                if (c == 'A')
                {
                    fitness++;
                }
            } 
            return fitness;
        }

        /**
         * Corresponds the chromosome encoding to the phenotype, which is a representation
         * that can be read, tested and evaluated by the main program.
         * @return a String with a length equal to the chromosome size, composed of "A"s
         * at the positions where the chromosome is 1 and "a"s at the posiitons
         * where the chromosome is 0
         */
        public override string GetPhenotype()
        {
            var phenotype = "";
            foreach (int i in mChromosome)
            {
                if (i == 0)
                {
                    phenotype += "a";
                }

                if (i == 1)
                {
                    phenotype += "A";
                }
            }

            return phenotype;
        }

        /**
         * Mutates a gene using inversion, random mutation or other methods.
         * This function is called after the mutation chance is rolled.
         * Mutation can occur (depending on the designer's wishes) to a parent
         * before reproduction takes place, an offspring at the time it is created,
         * or (more often) on a gene which will not produce any offspring afterwards.
         */
        public override void Mutate()
        {
            int randomIndex = Random.Range(0, mChromosome.Length);
            if (mChromosome[randomIndex] == 0)
            {
                mChromosome[randomIndex] = 1;
            }
            else
            {
                mChromosome[randomIndex] = 0;
            }
        }

        /**
         * Randomizes the numbers on the mChromosome array to values 0 or 1
         */
        public override void RandomizeChromosome()
        {
            for (int i = 0; i < mChromosome.Length; i++)
            {
                mChromosome[i] = Random.Range(0, 2);
            }
        }

        /**
         * Creates a number of offspring by combining (using crossover) the current
         * Gene's chromosome with another Gene's chromosome.
         * Usually two parents will produce an equal amount of offpsring, although
         * in other reproduction strategies the number of offspring produced depends
         * on the fitness of the parents.
         * @param other: the other parent we want to create offpsring from
         * @return Array of Gene offspring (default length of array is 2).
         * These offspring will need to be added to the next generation.
         */
        public override GeneAbstract<int>[] Reproduce(GeneAbstract<int> other)
        {
            GeneA[] offspring = new GeneA[2];
            offspring[0] = new GeneA();
            offspring[1] = new GeneA();

            int randomIndex = Random.Range(0, mChromosome.Length - 1);

            for (int i = 0; i < mChromosome.Length; i++)
            {
                if (i < randomIndex)
                {
                    offspring[0].mChromosome[i] = mChromosome[i];
                    offspring[1].mChromosome[i] = other.mChromosome[i];
                }
                else
                {
                    offspring[0].mChromosome[i] = other.mChromosome[i];
                    offspring[1].mChromosome[i] = mChromosome[i];
                }
            }

            return offspring;
        }
    }
}
