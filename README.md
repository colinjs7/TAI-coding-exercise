# TAI-coding-exercise

The main function of the RiskAssessment class can be outlined as:

1. Sort the original file by Id
2. Combine records for a given Id and calculate Risk
3. Write client record to file if above Risk Threshold
4. Sort output file by descending Risk and ascending Id

Since the files could be larger than the 2GB limit, the sorting at the beginning and end utilizes [ExternalMergeSorter](https://github.com/joseftw/jos.files/tree/develop/src/JOS.ExternalMergeSort) by Josef Ottosson.  His blog post about the code is [here](https://josef.codes/sorting-really-large-files-with-c-sharp/).  It is an implementation of the K-way merge algorithm.  From the blog:

>The algorithm works something like this:

>1. Split the large files in multiple small files
>2. Sort the small files
>3. Merge X small files to bigger files using a K-way merge.
>4. Repeat step 3 until you only have 1 file left.


ExternalMergeSort included the ability to include a custom IComparer<> which allowed for sorting both by Risk and by Id.
