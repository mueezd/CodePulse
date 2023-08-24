export interface UpdateBlogPost{
    title: string;
    shortDescription: string;
    content: string;
    featuredImageUrl: string;
    urlHandel: string;
    author: string;
    publishDate: Date;
    isVisible: boolean;
    categories: string[];
}