<img src="https://github.com/TobiasSpilker/Shape_AI/blob/main/Informative/Images/banner.png">

# Reasonably sized documentation


-----------------------------------------------------------------------------------------------------------------------------------------


**ABOUT:**

This is the documentation for Shape_AI. In short, Shape_AI is an image recognition model which uses a peculiar way of data preperation. The algorithm for converting images into readable data for neural networks is very efficient because it selects for the right amount of detail from an image while the output data stays very small in size. The advantage of this is a huge boost in efficiency. In this documentation we will gloss over the inner workers of the entire program, it consists of several parts. The first two parts are about 1: The Data preperation, and 2: The neural network itself.


-----------------------------------------------------------------------------------------------------------------------------------------


**DATA PREPERATION:**

The data is prepared in 4 or 5 stepts (depending if you count methods or not). The input consists of an image of any kind, any size, any color. The type however, must be .png. The eventual output is a 2 dimensional array of 1's and 0's, how these are encoded will be explained below:

<br/>

**Step 1 - Retrieving the image as is:**

First of all, the image needs to be retrieved as it is uploaded by the user. This is simply done be looking for the first image in the designated folder. The image may look something like this:

<img src="https://github.com/TobiasSpilker/Shape_AI/blob/main/Informative/Images/ExampleStep1.png" height="200" width="200" >

<br/>

**Step 2 - Compressing the image:**

Second, we want to compress an image, the reason being is that the quality of all the input varies. This is not acceptable for a neural network. Another reason is that we can still get all the relevant details even if the resolution is way smaller, for example 100px by 100px. Lastly, by lowering the resolution to a standard like 100px by 100px the speed of processing and training the model dramatically increases. Compression is done by mapping the existing bitmap onto a smaller one while utilizing several methods that keep the image details. After compression the example image will look like this:

<img src="https://github.com/TobiasSpilker/Shape_AI/blob/main/Informative/Images/ExampleStep2.png" height="200" width="200" >

<br/>

**Step 3 - Contouring the compressed image:**

Because the model is focused on efficiency, there is still some unnecessary data which need to be dealt with. This is color. Because the application is focused on image recognition we believe only outlines, contrast differences and shadows are relevant. This is where our 3th step comes in. This method will loop through every pixel of our already compressed image and will change the pixels to either: Completely black or completely white. It does this based on: The gamma given by the user and the average brightness of the picture (still in the works). After being processed the image will look like this:

<img src="https://github.com/TobiasSpilker/Shape_AI/blob/main/Informative/Images/ExampleStep3.png" height="200" width="200" >

<br/>

**Step 4 and 5 - Translating the image to binary:**

Lastly, the black and white image is given as input into these two methods. These will read the image from left to right (starting from the top, going to the bottom), and for every pixel, will store a 1 if the pixel is black and a 0 if the pixel is white. This information gets stored into a 2D array (step 4). Step 5 takes this array and changes this from in-memory to out-memory by writing this array into a text file. The text file may look like this:

<img src="https://github.com/TobiasSpilker/Shape_AI/blob/main/Informative/Images/ExampleStep5.png" height="200" width="200">

<br/>

-----------------------------------------------------------------------------------------------------------------------------------------
